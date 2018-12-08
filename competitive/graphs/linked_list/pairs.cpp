#include <iostream>
#include <utility>
#include <vector>

using namespace std;

#define DEBUG

int main(){

    int st = 0, fin = 0, n, result = -1;
    cin >> n;

    vector< vector<int> > matrix(n);
    vector<int> visit(n, -1);
    for (int i = 0; i < n; i++){
        matrix[i].resize(n);
        for (int j = 0; j < n; j++)
            cin >> matrix[i][j];
    }

    int start, finish;
    cin >> start >> finish;
    start--;
    finish--;

    vector< pair<int, int> > nodes;
    nodes.push_back(make_pair(start, 0));

    while (st <= fin && visit[finish] == -1){
        for (int i = 0; i < n; i++){
            if (matrix[i][nodes[st].first] == 1 && visit[i] == -1){
                nodes.push_back(make_pair(i, nodes[st].second + 1));
                visit[i] = nodes[st].second + 1;
                fin++;
            }
        }
        st++;
    }
    cout << visit[finish] << endl;
    return 0;
}