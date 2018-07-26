#include <iostream>
#include <vector>
#include <map>

using namespace std;

const int infinity = 0x7FFFFFFF;

class dijkstra_heap {
public:
    void add(pair<int, int> element) {
        array.push_back(element);

        int current_pos = array.size() - 1;
        int top = (current_pos - 1) / 2;
        while (less(array[current_pos], array[top]) && current_pos > 0) {
            swap(array[top], array[current_pos]);

            current_pos = top;
            top = (current_pos - 1) / 2;
        }
    }
    pair<int, int> up() {
        pair<int, int> result = array[0];

        swap(array[0], array[array.size() - 1]);
        array.pop_back();

        int current_pos = 0;
        while (current_pos * 2 + 1 < array.size()) {
            int min_index;
            if (current_pos * 2 + 2 == array.size()) {
                if (less(array[current_pos], array[current_pos * 2 + 1]))
                    break;
                min_index = current_pos * 2 + 1;
            }
            else {
                if (less(array[current_pos], array[current_pos * 2 + 1]) && less(array[current_pos], array[current_pos * 2 + 2]))
                    break;
                
                if (less(array[current_pos * 2 + 2], array[current_pos * 2 + 1]))
                    min_index = current_pos * 2 + 2;
                else
                    min_index = current_pos * 2 + 1;
            }
            swap(array[current_pos], array[min_index]);

            current_pos = min_index;
        }

        return result;
    }

    bool empty() {
        return array.empty();
    }
protected:
    vector<pair<int, int>> array;

    bool less(pair<int, int> a, pair<int, int> b) {
        return a.first < b.first;
    }
};

vector<int> dijkstra(vector<vector<pair<int, int>>>& routes, int start, int n) {
    vector<int> dist(n, infinity);
    vector<bool> visit(n, false);

    dist[start] = 0;
    dijkstra_heap heap;

    heap.add(make_pair(0, start));

    do {
        auto least = heap.up();
        int target = least.second;

        if (visit[target])
            continue;
        visit[target] = true;

        for (auto route : routes[target]) {
            int index = route.first;
            int value = route.second + dist[target];

            if (value < dist[index]) {
                dist[index] = value;
                heap.add(make_pair(dist[index], index));
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