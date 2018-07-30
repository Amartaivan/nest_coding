/**
    @file    square_root_solution1.cpp
    @author  Altantur Bayarsaikhan (altantur)
    @purpose Find square root of given non-negative integer X
    @version 1.0 15/11/17
*/
#include <iostream>
#include <fstream>
using namespace std;

int main(){
    // Read from test files
    // ifstream test_file;
    // test_file.open ("../test/test1.txt");
    // test_file.close();

    double N;
    cin >> N;

    // N is double and sqrt(int N) returns double
    // rounds up by 10^10(2^70) using binary search
    int iter = 100;
    double l = 0.0, r = N;
    while(iter > 0) {
        iter --;
        double mid = (l + r) * 0.5;
        if (mid * mid > N) r = mid;
        else l = mid;
    }

    cout << "Square root of " << N << " is : " << (l + r) * 0.5 << endl;
    return 0;
}
