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

    int N;
    cin >> N;

    // N is integer and sqrt(int N) returns integers
    // rounds up
    int sqrt_N = 1;
    while(sqrt_N * sqrt_N < N) {
        sqrt_N++;
    }
    cout << "Square root of " << N << " is : " << sqrt_N << endl;
    return 0;
}
