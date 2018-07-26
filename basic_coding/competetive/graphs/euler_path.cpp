#include <iostream>
#include <vector>
#include <set>

using namespace std;

void traverse(int paths, vector<int> edges, vector<vector<int>> routes, vector<set<int>> visit, int start = -1) {
    if (start == -1) {
        start = 0;
        //Pick first odd vertex
        while (start < edges.size()) {
            if (edges[start] % 2 == 1) {
                traverse(paths, edges, routes, visit, start);
                return;
            }
            ++start;
        }

        return;
    }
    if (paths == 0)
        return;

    cout << start + 1 << endl;

    set<pair<int, int>> routes_next;
    //Find next vertex
    int i = 0, next = 0;
    while (i < routes[start].size()) {
        if (i != start)
            if (visit[start].find(routes[start][i]) == visit[start].end())
                routes_next.insert(make_pair(edges[routes[start][i]], routes[start][i]));
        ++i;
    }

    if (routes_next.empty())
        return;

    auto max = *routes_next.rbegin();
    next = max.second;

    --edges[start];
    --edges[next];
    visit[start].insert(next);
    visit[next].insert(start);

    traverse(paths - 2, edges, routes, visit, next);
}

int main() {
    int n, m, x, y, paths;
    cin >> n >> m;

    paths = m * 2 + 2;

    vector<int> edges(n);
    vector<vector<int>> routes(m);

    for (int i = 0; i < m; i++) {
        cin >> x >> y;

        --x;
        --y;

        routes[x].push_back(y);
        routes[y].push_back(x);

        ++edges[x];
        ++edges[y];
    }

    vector<set<int>> visit(n);
    traverse(paths, edges, routes, visit);
    return 0;
}