/*
Дано прямоугольное поле размером n*m клеток. Можно совершать шаги длиной в одну клетку вправо, вниз или по диагонали вправо-вниз. 
В каждой клетке записано некоторое натуральное число. Необходимо попасть из верхней левой клетки в правую нижнюю. 
Вес маршрута вычисляется как сумма чисел со всех посещенных клеток. Необходимо найти маршрут с минимальным весом.
*/

#include <iostream>
#include <vector>

using namespace std;

int solve(vector<vector<int>> matrix, int i, int j) {
    if (i == 0 && j == 0)
        return matrix[i][j];
    if (i == 0)
        return matrix[i][j] + solve(matrix, i, j - 1);
    if (j == 0)
        return matrix[i][j] + solve(matrix, i - 1, j);
    return min(solve(matrix, i - 1, j), min(solve(matrix, i - 1, j - 1), solve(matrix, i, j - 1))) + matrix[i][j];
}

const int up = 0;
const int _left = 1;
const int upleft = 2;

int solve_route(vector<vector<int>> matrix, int i, int j, vector<vector<int>>& route) {
    if (i == 0 && j == 0)
        return matrix[i][j];
    if (i == 0){
        route[i][j] = _left;
        return matrix[i][j] + solve(matrix, i, j - 1);
    }
    if (j == 0){
        route[i][j] = up;
        return matrix[i][j] + solve(matrix, i - 1, j);
    }

    int a = solve_route(matrix, i - 1, j, route);
    int b = solve_route(matrix, i - 1, j - 1, route);
    int c = solve_route(matrix, i, j - 1, route);

    int minval = min(a, min(b, c));
    if (minval == a)
        route[i][j] = up;
    else if (minval == b)
        route[i][j] = upleft;
    else
        route[i][j] = _left;

    return minval + matrix[i][j];
}

int main() {
    int n, m;
    cin >> n >> m;

    vector<vector<int>> matrix(n, vector<int>(m));
    vector<vector<int>> route(n, vector<int>(m));
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            cin >> matrix[i][j];

    cout << solve_route(matrix, n - 1, m - 1, route) << endl;
    for (auto row : route){
        for (int val : row)
            cout << val << ' ';
        cout << endl;
    }
    return 0;
}