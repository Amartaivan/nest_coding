#include <iostream>
#include <vector>

using namespace std;

typedef vector<vector<int>> matrix_t;

const int unset = -1;
const int no_way = -2;

void wave_set(matrix_t& matrix, vector<pair<int, int>>& queue, int i, int j, int n, int m, int value = 0) {
    if (i >= 0 && i < n && j >= 0 && j < m && (matrix[i][j] == unset || matrix[i][j] > value)){
        matrix[i][j] = value;

        queue.push_back(make_pair(i, j));
    }
}
void wave(matrix_t& matrix, int i, int j, int n, int m, int depth = 0) {
    vector<pair<int, int>> queue;

    wave_set(matrix, queue, i - 1, j, n, m, depth + 1);
    wave_set(matrix, queue, i + 1, j, n, m, depth + 1);
    wave_set(matrix, queue, i, j - 1, n, m, depth + 1);
    wave_set(matrix, queue, i, j + 1, n, m, depth + 1);

    for (auto coord : queue)
        wave(matrix, coord.first, coord.second, n, m, depth + 1);
}

int main() {
    int n, m;
    cin >> n >> m;

    matrix_t matrix(n, vector<int>(m));
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            cin >> matrix[i][j];

    int start_i, start_j;
    cin >> start_i >> start_j;

    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++) {
            if (matrix[i][j] == 1)
                matrix[i][j] = unset;
            if (matrix[i][j] == 0)
                matrix[i][j] = no_way;
        }

    matrix[start_i][start_j] = 0;
    wave(matrix, start_i, start_j, n, m);

    for (auto row : matrix){
        for (int val : row)
            cout << (val == no_way ? 0 : val) << ' ';
        cout << endl;
    }
    return 0;
}