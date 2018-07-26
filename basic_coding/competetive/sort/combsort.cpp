#include <iostream>
#include <vector>

using namespace std;

vector<int> combsort(vector<int> array){

    int gap = array.size() * 10 / 13; //Gap factor
    while (gap > 0){
        int swapped = 0;
        for (int i = 0; i < array.size() - gap; i++){
            if (array[i + gap] < array[i]){
                swap(array[gap + i], array[i]);
                swapped = 1;
            }
        }
        if ((gap = gap * 10 / 13) == 0)
            gap = swapped;
    }
    return array;
}

int main(){

    int n;
    cin >> n;
    vector<int> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];

    A = combsort(A);

    for (int a : A)
        cout << a << ' ';
    cout << endl;
    return 0;
}