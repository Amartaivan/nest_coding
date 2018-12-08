#include <iostream>
#include <vector>

using namespace std;

void bfs(vector< vector<int> > matrix, vector<int>& visit, int start, int end, int n){

    int level = 0;
    while (visit[end] == -1){
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

    int n, start, target;
    cin >> n;

    vector< vector<int> > matrix(n);
    vector<int> visit(n, -1);

    for (int i = 0; i < n; i++){
        matrix[i].resize(n);
        for (int j = 0; j < n; j++)
            cin >> matrix[i][j];
    }
    cin >> start >> target;
    start--;
    target--;
    visit[start] = 0;
    bfs(matrix, visit, start, target, n);
    cout << visit[target] << endl;
    return 0;
}