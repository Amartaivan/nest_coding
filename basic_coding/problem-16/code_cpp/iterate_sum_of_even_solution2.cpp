/**
    @file    iterate_sum_solution1.cpp
    @author  Ochirgarid Chinzorig (Ochirgarid)
    @purpose Find sum of 2 integers range
    @version 1.0 30/07/18 
*/
#include <iostream>
#include <fstream>

using namespace std;

int main(){
    ifstream test_file;
    int n = 0, m = 0, sum;

    // Read from test files
    test_file.open ("../test/test1.txt");
    test_file >> m;
    test_file >> n;
    test_file.close();

    // Using Arithmetic sequence to find sum
    // only works when n >= m
    // sum = 4 + 6 + 8 + 10
    // = (2 + 4 + 6 + 8 + 10) - (2)
    // = 2 * (1 + 2 + 3 + 4 + 5) - 2 * (1)
    sum = (n / 2) * (n / 2 + 1) - ((m - 1) / 2) * ((m - 1) / 2 + 1);

    cout << "Sum of " << m << ".." << n << " : " << sum << endl;

    return 0;
}

