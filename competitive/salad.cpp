#include <iostream>
#include <vector>

using namespace std;

void salad(vector<int> array) {
    for (int el : array)
        cout << el << ' ';
    cout << endl;

    int i = array.size() - 1;

    while (i > 0 && array[i - 1] >= array[i])
        --i;
    if (i == 0)
        return;

    --i;
    int j = array.size() - 1;
    while (j > i && array[j] <= array[i])
        --j;

    swap(array[i], array[j]);

    i++;
    j = array.size() - 1;
    while (i <= j) {
        swap(array[i], array[j]);

        ++i;
        --j;
    }

    salad(array);
}

int main() {
    int n;
    cin >> n;

    vector<int> array(n);
    for (int i = 1; i <= n; i++)
        array[i - 1] = i;

    salad(array);

    return 0;
}