#include <iostream>

using namespace std;

int main(){

    int n, i, j, a;
    bool result = true;
    cin >> n;
    int A[n][n];

    for (i = 0; i < n; i++)
        for (j = 0; j < n; j++)
            cin >> A[i][j];

    for (a = n - 1; a > 0; a--)
        for (i = a - 1; i >= 0; i--)
            if (A[a][i] != A[i][a])
                result = false;
    cout << (result ? "YES" : "NO") << endl;
    return 0;
}
