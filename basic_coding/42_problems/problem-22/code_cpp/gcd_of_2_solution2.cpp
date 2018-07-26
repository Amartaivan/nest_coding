/**
    @file    gcd_of_2_solution2.cpp
    @author  Altantur Bayarsaikhan (altantur)
    @purpose Find greatest common divider and least common multiple of 2 integers
    @version 1.0 26/10/17 
*/
#include <iostream>
#include <fstream>
#include <cmath>
using namespace std;

int main(){
    ifstream test_file;
    long gcd = 1, a = 0, b = 0, lcm, ans;
    int mod = 0;

    // Read from test files
    test_file.open ("../test/test1.txt");
    test_file >> a;
    test_file >> b;
    test_file.close();

    // To use for LCM later 
    ans = a * b;

    // Eliminate non-negative to positive number
    a = abs(a); 
    b = abs(b); 

    // Using Euclidean method
    do {
        if(a < b){
            b = b % a;
        }else {
            a = a % b;
        }
    } while(a != 0 && b != 0);
    gcd = a + b;

    // Least common multiple
    lcm = ans / gcd;

    cout << "Greatest common divider is : " << gcd << endl;
    cout << "Least common multiple is : " << lcm << endl;

    return 0;
}
