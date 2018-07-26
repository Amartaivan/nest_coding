#include <iostream>
#include <vector>
#include <set>

using namespace std;

const int infinity = 0x7FFFFFFF;

vector<int> dijkstra(vector<vector<pair<int, int>>>& routes, int start, int n) {
    vector<int> dist(n, infinity);
    vector<bool> visit(n, false);

    dist[start] = 0;
    set<pair<int, int>> heap;

    heap.insert(make_pair(0, start));

    do {
        auto least = *heap.begin();
        heap.erase(heap.begin());
        int target = least.second;

        if (visit[target])
            continue;
        visit[target] = true;

        for (auto route : routes[target]) {
            int index = route.first;
            int value = route.second + dist[target];

            if (value < dist[index]) {
                dist[index] = value;
                heap.insert(make_pair(dist[index], index));
            }
        }
    } while (!heap.empty());

    return dist;
}

int main() {
    int T;
    cin >> T;
    while (T--) {
        int n, m;
        cin >> n >> m;

        vector<vector<pair<int, int>>> routes(n);
        for (int i = 0; i < m; i++) {
            int tmp_x, tmp_y, tmp_z;
            cin >> tmp_x >> tmp_y >> tmp_z;

            tmp_x--;
            tmp_y--;

            routes[tmp_x].push_back(make_pair(tmp_y, tmp_z));
            routes[tmp_y].push_back(make_pair(tmp_x, tmp_z));
        }

        int start;
        cin >> start;
        --start;

        auto result = dijkstra(routes, start, n);
        for (int i = 0; i < n; i++) {
            if (i == start)
                continue;
            if (result[i] == infinity)
                cout << "-1 ";
            else
                cout << result[i] << ' ';
        }
        cout << endl;
    }
    return 0;
}