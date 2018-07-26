#include <iostream>
#include <vector>

#include <cmath>

using namespace std;

typedef vector<int> heap_t;

heap_t heap_build(vector<int> array) {
    heap_t result;
    int heap_size = 0;

    for (int element : array) {
        result.push_back(element);
        heap_size++;

        int current_pos = heap_size - 1;
        int top = (current_pos - 1) / 2;
        while (result[top] > result[current_pos] && current_pos > 0) {
            swap(result[top], result[current_pos]);

            current_pos = top;
            top = (current_pos - 1) / 2;
        }

    }

    return result;
}
int heap_up(heap_t& heap) {
    int result = heap[0];

    swap(heap[0], heap[heap.size() - 1]);
    heap.pop_back();

    return result;
}
void heap_down(heap_t& heap) {
    int current_pos = 0;

    while (current_pos * 2 + 1 < heap.size()) {
        
        int min_index;
        if (current_pos * 2 + 2 == heap.size()) {
            if (heap[current_pos * 2 + 1] > heap[current_pos])
                break;
            min_index = current_pos * 2 + 1;
        }
        else {
            if (heap[current_pos * 2 + 1] > heap[current_pos] && heap[current_pos * 2 + 2] > heap[current_pos])
                break;
            
            if (heap[current_pos * 2 + 1] > heap[current_pos * 2 + 2])
                min_index = current_pos * 2 + 2;
            else
                min_index = current_pos * 2 + 1;
        }
        swap(heap[current_pos], heap[min_index]);

        current_pos = min_index;
    }
}

vector<int> heapsort(vector<int> array) {
    vector<int> result;

    heap_t heap = heap_build(array);
    while (!heap.empty()) {
        result.push_back(heap_up(heap));
        heap_down(heap);
    }

    return result;
}

int main() {
    int n;
    cin >> n;

    vector<int> array(n);
    for (int i = 0; i < n; i++)
        cin >> array[i];

    array = heapsort(array);

    for (int element : array)
        cout << element << ' ';
    cout << endl;
    return 0;
}