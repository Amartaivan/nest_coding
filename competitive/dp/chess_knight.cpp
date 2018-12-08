#include <iostream>
#include <vector>

using namespace std;

inline void add(vector<vector<int>>& matrix, int n, int m, int i, int j) {
    if (i < 0 || i >= n || j < 0 || j >= m)
        return;
    matrix[i][j]++;
}
void solve(vector<vector<int>>& matrix, int n, int m, int i = 0, int j = 0) {
    if (i < 0 || i >= n || j < 0 || j >= m)
        return;
    
    matrix[i][j]++;

    solve(matrix, n, m, i - 1, j + 2);
    solve(matrix, n, m, i + 1, j + 2);
    solve(matrix, n, m, i + 2, j - 1);
    solve(matrix, n, m, i + 2, j + 1);
}

int main() {
    int n, m;
    cin >> n >> m;

    vector<vector<int>> matrix(n, vector<int>(m, 0));

    solve(matrix, n, m);

    cout << matrix[n - 1][m - 1] << endl;
    return 0;
}