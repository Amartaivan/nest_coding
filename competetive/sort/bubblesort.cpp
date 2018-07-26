//Descending bubble sort
#include <iostream>

using namespace std;

int main(){

    int n;
    cin >> n;
    int A[n], last = n;

    for (int i = 0; i < n; i++)
        cin >> A[i];

    bool is_sorted = false;
    do {
        is_sorted = true;
        for (int i = 1; i < n; i++)
            for (int j = 0; j < n - i; j++)
                if (A[j] > A[j + 1]){
                    swap(A[j], A[j + 1]);
                    is_sorted = false;
                }
    } while (!is_sorted);

    for (auto a : A)
        cout << a << ' ';
    return 0;
}