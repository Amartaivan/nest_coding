#include <iostream>

using namespace std;

int egcd(int a, int b, int& x, int& y) {
    if (a == 0) {
        x = 0;
        y = 1;

        return b;
    }

    int x1, y1, g;
    g = egcd(b % a, a, x1, y1);

    y = x1;
    x = y1 - (b / a) * x1;

    return g;
}

int main() {
    int a, b, x, y, g;
    cin >> a >> b;

    g = egcd(a, b, x, y);
    cout << x << " * " << a << " + " << y << " * " << b << " = " << g << endl;
    return 0;
}