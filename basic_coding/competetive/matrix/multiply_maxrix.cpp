#include "matrix.h"

matrix_t multiply(matrix_t a, matrix_t b){
    int n = a.size();

    matrix_t result(n, vector<int>(n));

    for (int i = 0; i < n; i++){
        for (int j = 0; j < n; j++){
            int sum = 0;

            for (int k = 0; k < n; k++)
                sum += a[i][k] * b[k][j];

            result[i][j] = sum;
        }
    }

    return result;
}

int main(){

    int n;
    cin >> n;
    matrix_t m1(n, vector<int>(n)), m2(n, vector<int>(n));

    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            cin >> m1[i][j];
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++)
            cin >> m2[i][j];

    matrix_t result = multiply(m1, m2);
    for (auto a : result){
        for (auto b : a)
            cout << b << ' ';
        cout << endl;
    }
    return 0;
}