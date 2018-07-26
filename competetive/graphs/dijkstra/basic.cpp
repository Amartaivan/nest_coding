#include <iostream>
#include <vector>
#include <set>

using namespace std;

struct route {
    int x, y, z;
};

//Vertex

void dijkstra(vector<route> routes, vector<pair<int, int>>& result, int start, set<int>& visit, int weight = 0) {
    if (visit.find(start) != visit.end())
        return;
    visit.insert(start);

    vector<int> next;

    for (route curr_route : routes) {
        if (curr_route.x == start && visit.find(curr_route.y) == visit.end()) {
            result[curr_route.y].first = min(result[curr_route.y].first, weight + curr_route.z);
            result[curr_route.y].second = start;

            next.push_back(curr_route.y);
        }
        else if (curr_route.y == start && visit.find(curr_route.x) == visit.end()) {
            result[curr_route.x].first = min(result[curr_route.x].first, weight + curr_route.z);
            result[curr_route.x].second = start;
            
            next.push_back(curr_route.x);
        }
    }

    for (int next_start : next)
        dijkstra(routes, result, next_start, visit, result[next_start].first);
}

vector<int> restore_path(vector<pair<int, int>> routes, int start, int target) {
    vector<int> result;

    int curr_node = target;
    while (curr_node != -1) {
        result.push_back(curr_node);

        curr_node = routes[curr_node].second;
    }

    return vector<int>(result.rbegin(), result.rend());
}

int main() {
    int n, m, start;
    cin >> n >> m >> start;

    --start;

    vector<route> routes(m);
    for (int i = 0; i < m; i++){
        cin >> routes[i].x >> routes[i].y >> routes[i].z;

        --routes[i].x;
        --routes[i].y;
    }

    vector<pair<int, int>> result(n, make_pair(0x7FFFFFFF, -1));
    result[start].first = 0;

    set<int> visit;

    dijkstra(routes, result, start, visit);   
    
    for (int i = 0; i < n; i++){
        cout << start + 1 << " -> " << i + 1 << ": " << result[i].first << endl;

        for (int node : restore_path(result, start, i))
            cout << node + 1 << ' ';
        cout << endl;
    }
    return 0;
}