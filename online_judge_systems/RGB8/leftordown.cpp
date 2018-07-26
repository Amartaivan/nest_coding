#include <iostream>

using namespace std;

int main(){
    int i, j, n, m;
    cin >> n >> m;
    long A[n][m];

    m--;
    for (i = 0; i < n; i++)
        A[i][m] = 1;
    for (j = m; j >= 0; j--)
        A[0][j] = 1;
    
    m--;
    for (i = 1; i < n; i++)
        for (j = m; j >= 0; j--)
            A[i][j] = A[i - 1][j] + A[i][j + 1];

    cout << A[n - 1][0] << endl;

    return 0;
}