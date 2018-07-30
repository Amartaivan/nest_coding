/**
    @file    dividers_sum_max_solution1.cpp
    @author  Altantur Bayarsaikhan (altantur)
    @purpose Find sum of 2 integers
    @version 1.0 10/11/17 
*/
#include <iostream>
#include <fstream>
#include <cmath>
using namespace std;

int main(){
    // Read from test files
    // ifstream test_file;
    // test_file.open ("../test/test1.txt");
    // test_file.close();

    int n;
    cin >> n;

    // linear check solution
    int answer_int = 0, answer_sum = 0;
    for(int i = 1; i <= n; i++) {
        int sum_of_divisor = 0;
        for(int d = 1; d <= i; d++) {
            if(i % d == 0) {
                sum_of_divisor += d;
            }
        }
        if(sum_of_divisor > answer_sum) {
            answer_int = i;
            answer_sum = sum_of_divisor;
        }
    }

    cout << "Number with max sum of divisor : " << answer_int << endl;
    cout << "Sum of its divisors : " << answer_sum << endl;

    return 0;
}

