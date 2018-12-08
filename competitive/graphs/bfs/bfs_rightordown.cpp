#include <iostream>
#include <vector>

using namespace std;

int bfs(vector< vector<int> > matrix, int n, int m, int start_i, int start_j, int money){
    if (start_i == n - 1 && start_j == m - 1)
        return money + matrix[start_i][start_j];
    
    money += matrix[start_i][start_j];
    if (start_i == n - 1)
        return bfs(matrix, n, m, start_i, start_j + 1, money);
    else if (start_j == m - 1)
        return bfs(matrix, n, m, start_i + 1, start_j, money);
    else
        return min(bfs(matrix, n, m, start_i + 1, start_j, money), bfs(matrix, n, m, start_i, start_j + 1, money));
}

int main(){

    int n, m;
    cin >> n >> m;

    vector< vector<int> > matrix(n);
    for (int i = 0; i < n; i++){
        matrix[i].resize(m);
        for (int j = 0; j < m; j++)
            cin >> matrix[i][j];
    }

    cout << bfs(matrix, n, m, 0, 0, 0) << endl;

    return 0;
}