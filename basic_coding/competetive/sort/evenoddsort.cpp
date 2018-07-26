#include <iostream>
#include <vector>

using namespace std;

vector<int> evenoddsort(vector<int> array){

    for (int i = 0; i < array.size(); i++){
        int j = i % 2;
        while (j < array.size() - 1){
            if (array[j] > array[j + 1])
                swap(array[j], array[j + 1]);

            j += 2;
        }
    }
    return array;
}

int main(){

    int n;
    cin >> n;
    vector<int> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];

    A = evenoddsort(A);

    for (int a : A)
        cout << a << ' ';
    cout << endl;
    return 0;
}