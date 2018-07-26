#include <iostream>
#include <vector>

using namespace std;

typedef vector<vector<int>> matrix_t;

const int infinity = 0x7000000;

int main() {
    int n;
    cin >> n;

    matrix_t matrix(n, vector<int>(n));
    for (int i = 0; i < n; i++)
        for (int j = 0; j < n; j++){
            cin >> matrix[i][j];
            if (matrix[i][j] == 0)
                matrix[i][j] = infinity;
        }

    for (int k = 0; k < n; k++)
        for (int i = 0; i < n; i++) 
            for (int j = 0; j < n; j++)
                matrix[i][j] = min(matrix[i][j], matrix[i][k] + matrix[k][j]);

    cout << "---" << endl;
    for (auto row : matrix){
        for (int val : row)
            cout << (val == infinity ? 0 : val) << ' ';
        cout << endl;       
    }

    return 0;
}