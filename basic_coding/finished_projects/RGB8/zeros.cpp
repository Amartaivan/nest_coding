#include <iostream>

using namespace std;

int main(){

    int n, m, i, j, resulti = -1, resultj = 0;
    cin >> n >> m;
    int A[n][m];

    for (i = 0; i < n; i++)
        for (j = 0; j < m; j++)
            cin >> A[i][j];

    for (i = 0; i < n; i++)
        for (j = 0; j < m; j++)
            if (A[i][j] == 0 && resulti == -1){
                resulti = i;
                resultj = j;
            }

    cout << resulti + 1 << ' ' << resultj + 1 << endl;
    return 0;
}
