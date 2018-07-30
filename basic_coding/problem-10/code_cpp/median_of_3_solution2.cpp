/**
    @file    median_of_3_solution2.cpp
    @author  Ochirgarid Chinzorig (ochirgarid)
    @purpose Find median number 3 integers
    @version 1.0 30/07/18 
*/
#include <iostream>
#include <fstream>
using namespace std;

int main(){
    ifstream test_file;
    int a = 0, b = 0, c = 0, median = 0;
    int temp = 0;

    // Read from test files
    test_file.open ("../test/test1.txt");
    test_file >> a;
    test_file >> b;
    test_file >> c;
    test_file.close();

    // Find comparing two
    median = (a + b + c) - (min(a,min(b,c)) + max(a,max(b,c)));
    cout << "Median number is : " << median << endl;

    return 0;
}

