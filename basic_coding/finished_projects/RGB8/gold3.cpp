#include <iostream>

using namespace std;

int main(){
    int command, n, m, x, i, j, sum, tmp;
    cin >> n >> m;
    int A[n][m];

    for (i = 0; i < n; i++)
        for (j = 0; j < m; j++)
            cin >> A[i][j];
    cin >> x;

    i = 0;
    j = 0;
    sum = A[0][0];
    A[0][0] = 0;
    for (tmp = 0; tmp < x - 1; tmp++){
        cin >> command;

        switch (command){
            case 1:
                j++;
                break;
            case 2:
                i--;
                break;
            case 3:
                j--;
                break;
            case 4:
                i++;
                break;
        }
        
        sum += A[i][j];
        A[i][j] = 0;
    }

    cout << sum << endl;
    return 0;
}