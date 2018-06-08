//
//  HeavyCalculationsSyncTest.swift
//  PerfTester-Xcode
//
//  Created by Patryk Średziński on 08.06.2018.
//  Copyright © 2018 private.perftesterxcode. All rights reserved.
//

import UIKit

class HeavyCalculationsSyncTest : Test {

    override init() {
        super.init()
        title = "Heavy Sync Calculations"
        desc = "Doing a lot of math operations on a huge matrixes working on a main thread."
        imageName = "Maths"
        parameters = [10000, 20000, 50000, 100000, 200000, 500000, 1000000, 2000000, 5000000, 10000000]
    }
    
    override func prepare(param: Double) {
        
    }
    
    override func doJob(param: Double) {
        for _ in 0...Int(param) {
            let testArr = [32.521, 643.321, 74532.21, 634.12, 0.042021, -532.321, -321.6732, -2213.32535]
            var _ = multiplyArray(testArr)
        }
        super.finishJob(param: param)
    }
    
    private func multiplyArray(_ array: [Double]) -> [Double] {
        var finArr: [Double] = []
        for i in 0...array.count {
            finArr[i] = array[i] * array[i]
        }
        return finArr
    }
    
}
