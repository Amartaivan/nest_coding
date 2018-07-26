#include <iostream>
#include <vector>

using namespace std;

vector<int> quicksort(vector<int> array){
    vector<int> less, equal, greater, result = array;

    if (array.size() > 1){
        int pivot = (array[0] + array[array.size() / 2] 
                        + array[array.size() - 1]) / 3;
        for (int element : array){
            if (element < pivot)
                less.push_back(element);
            else if (element == pivot)
                equal.push_back(element);
            else
                greater.push_back(element);
        }

        result.clear();
        less = quicksort(less);
        greater = quicksort(greater);
        for (int element : less)
            result.push_back(element);
        for (int element : equal)
            result.push_back(element);
        for (int element : greater)
            result.push_back(element);
    }
    return result;
}

int main(){

    int n;
    cin >> n;
    vector<int> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];
    
    A = quicksort(A);

    for (int a : A)
        cout << a << ' ';
    cout << endl;
    return 0;
}