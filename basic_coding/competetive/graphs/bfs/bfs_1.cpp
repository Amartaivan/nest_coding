#include <iostream>
#include <vector>

using namespace std;

void bfs(vector< vector<int> > matrix, vector<int>& visit, int start, int end, int n){
    int level = 0;
    while (visit[end] == -1 && level <= n){
        for (int st = 0; st < n; st++){
            if (visit[st] == level){
                for (int i = 0; i < n; i++)
                    if (matrix[st][i] == 1 && visit[i] == -1)
                        visit[i] = level + 1;
                    }
            }
        level++;    
    }
}

int main(){
    int n;
    cin >> n;

    vector< vector<int> > matrix(n);
    for (int i = 0; i < n; i++){
        matrix[i].resize(n);
        for (int j = 0; j < n; j++)
            cin >> matrix[i][j];
    }

    for (int start = 0; start < n - 1; start++){
        for (int target = start + 1; target < n; target++){
            vector<int> visit(n, -1);
            visit[start] = 0;
            bfs(matrix, visit, start, target, n);
            cout << "From " << start + 1 << " to " << target + 1 << ": " << 
                (visit[target] == -1 ? "impossible" : to_string(visit[target]) + " moves") << endl;
        }
    }

    return 0;
}