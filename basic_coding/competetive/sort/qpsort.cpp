#include <iostream>
#include <vector>
#include <thread>
#include <future>

using namespace std;

inline int log2(int n) {
    int result = 0;
    while (n > 0) {
        n >>= 2;
        result++;
    }
    return result;
}

vector<int> qpsort(vector<int> array, int depth = 0, int max_depth = -1) {
    if (max_depth == -1)
        max_depth = log2(array.size());

    //Insertion sort
    if (array.size() <= 8) {
        for (int i = 1; i < array.size(); i++) {
            int j = i;
            while (array[j] < array[j - 1] && j > 0) {
                swap(array[j], array[j - 1]);
                j--;
            }
        }
        
        return array;
    }
    
    //Merge sort
    if (depth > max_depth) {
        int mid = array.size() / 2;

        auto l = qpsort(vector<int>(array.begin(), array.begin() + mid), depth, max_depth);
        auto r = qpsort(vector<int>(array.begin() + mid, array.end()), depth, max_depth);

        int i_l = 0, i_r = 0;
        while (i_l < l.size() && i_r < r.size()) {
            if (l[i_l] > r[i_r]){
                array[i_l + i_r] = r[i_r];
                i_r++;
            }
            else {
                array[i_l + i_r] = l[i_l];
                i_l++;
            }
        }
        while (i_l < l.size()) {
            array[i_l + i_r] = l[i_l];
            i_l++;
        }
        while (i_r < l.size()) {
            array[i_l + i_r] = r[i_r];
            i_r++;
        }

        return array;
    }

    //Median-of-3 quicksort
    int pivot = (array[0] + array[(array.size() - 1) / 2] + array[array.size() - 1]) / 3, equal_count = 0;
    vector<int> less, greater;

    for (int element : array)
        if (element < pivot)
            less.push_back(element);
        else if (element > pivot)
            greater.push_back(element);
        else
            equal_count++;

    less = qpsort(less, depth + 1, max_depth);
    greater = qpsort(greater, depth + 1, max_depth);

    int index = 0;
    for (int i = 0; i < less.size(); i++) {
        array[index] = less[i];
        ++index;
    }
    for (int i = 0; i < equal_count; i++) {
        array[index] = pivot;
        ++index;
    }
    for (int i = 0; i < greater.size(); i++) {
        array[index] = greater[i];
        ++index;
    }

    return array;
}

int main() {
    int n;
    cin >> n;

    vector<int> array(n);
    for (int i = 0; i < n; i++)
        cin >> array[i];

    array = qpsort(array);
    for (int el : array)
        cout << el << ' ';
    cout << endl;
    return 0;
}