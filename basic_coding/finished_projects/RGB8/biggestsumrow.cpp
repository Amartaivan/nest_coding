#include <iostream>

using namespace std;

int main(){

    int n, m, max = 0, max_i = 0;
    cin >> n >> m; //x, y

    int A[n][m];


    for (int i = 0; i < n; i++){
        for (int j = 0; j < m; j++){
            cin >> A[i][j];
        }
    }
    for (int j = 0; j < m; j++)
        max += A[0][j];
        
    for (int i = 1; i < n; i++){
        int temp_bag = 0;
        for (int j = 0; j < m; j++){
            temp_bag += A[i][j];
        }
        if (temp_bag >= max){
            max = temp_bag;
            max_i = i;
        }
    }

    cout << max_i + 1 << endl;
    return 0;
}