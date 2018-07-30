/**
    @file    chess_coordination_color_solution1.cpp
    @author  Ochirgarid Chinzorig (Ochirgarid)
    @purpose Find out if given 2 chess coordination is in same color
    @version 1.0 30/07/18 
*/
#include <iostream>
#include <fstream>
#include <string>
using namespace std;

int main(){
    // Read from test files
    // ifstream test_file;
    // test_file.open ("../test/test1.txt");
    // test_file.close();

    // Getting user input
    int xa, xb, ya, yb;
    bool same = false;
    char a[2], b[2];
    cin >> a >> b;
    xa = a[0] - 'A' + 1;
    xb = b[0] - 'A' + 1;
    ya = a[1] - '0' + 1;
    yb = b[1] - '0' + 1;

    // rows color pattern repeats in every 2 row
    // same rule at column
    xa = xa % 2;
    xb = xb % 2;
    ya = ya % 2;
    yb = yb % 2;
    if(xa == xb && ya == yb) {
        same = true;
    }
    if(xa != xb && ya != yb) {
        same = true;
    }
    // more short way
    // same = ~(((a[0]%2)==(b[0]%2))^((a[1]%2)==((b[1]%2))))

    if(same) cout << "Yes" << endl;
    else cout << "No" << endl;

    return 0;
}

