#include <iostream>
#include <vector>

using namespace std;

template <typename T, typename Compare = std::less<T>>
class heap {
public:
    heap() {};
    heap(Compare comp) {
        __comp = comp;
    }

    void add(T element) {
        array.push_back(element);

        int current_pos = array.size() - 1;
        int top = (current_pos - 1) / 2;
        while (less(array[current_pos], array[top]) && current_pos > 0) {
            swap(array[top], array[current_pos]);

            current_pos = top;
            top = (current_pos - 1) / 2;
        }
    }
    T up() {
        T result = array[0];

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

    auto begin() {
        return array.begin();
    }
    auto end() {
        return array.end();
    }
protected:
    Compare __comp;
    vector<T> array;

    virtual bool less(T a, T b) {
        return __comp(a, b);
    }
};

int main() {
    heap<int> heap;

    int tmp;
    while (true) {
        cin >> tmp;

        heap.add(tmp);

        for (int el : heap)
            cout << el << ' ';
        cout << endl;
    }
    return 0;
}