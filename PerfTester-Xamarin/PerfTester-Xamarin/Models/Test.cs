﻿using System;
using System.Diagnostics;
using Foundation;
using CoreFoundation;

namespace PerfTesterXamarin.Models
{
    public abstract class Test : NSObject
    {
        const int NSEC_PER_SEC = 1000000000;

        public abstract string Title { get; }
        public abstract string Desc { get; }
        public abstract string ImageName { get; }
        public abstract double[] Parameters { get; }

        public abstract void Prepare(double param);
        public abstract void DoJob(double param);

        public long[] Results;
        public bool[] IsTestRunning;

        Stopwatch[] Timers;
        Action<long[]> UpdateBlock;
        int CurrentParam = 0;

        public void Start(Action<long[]> updateBlock)
        {
            UpdateBlock = updateBlock;
            ResetTest();
            StartSingleVariantAsync(CurrentParam);
        }

        void ResetTest()
        {
            CurrentParam = 0;
            Results = new long[Parameters.Length];
            Timers = new Stopwatch[Parameters.Length];
            UpdateBlock(Results);
        }

        void StartSingleVariantAsync(int paramIndex)
        {
            var afterTime = new DispatchTime(DispatchTime.Now, (long)0.1 * NSEC_PER_SEC);
            DispatchQueue.MainQueue.DispatchAfter(afterTime, () => 
            {
                StartSingleVariant(paramIndex);
            });
        }

        void StartSingleVariant(int paramIndex)
        {
            double param = Parameters[paramIndex];
            Prepare(param);
            Timers[paramIndex] = Stopwatch.StartNew();
            DoJob(param);
        }

        public void FinishJob(double param)
        {
            Timers[CurrentParam].Stop();
            Results[CurrentParam] = Timers[CurrentParam].ElapsedMilliseconds;
            UpdateBlock(Results);
            CurrentParam++;
            if (CurrentParam < Parameters.Length)
            {
                StartSingleVariantAsync(CurrentParam);
            }
        }
    }
}
