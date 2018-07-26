#include <iostream>

using namespace std;

int main(){

    int n;
    cin >> n;
    int A[n];

    for (int i = 0; i < n; i++)
        cin >> A[i];

    int i = 0, j = 0;
    bool is_sorted = false;
    do {
        is_sorted = true;
        for (i = 0; i < n - 1; i++){
            if (A[i] > A[i + 1]){
                swap(A[i], A[i + 1]);
                is_sorted = false;
            }
        }
        for (i--; i >= j; i--){
            if (A[i] > A[i + 1]){
                swap(A[i], A[i + 1]);
                is_sorted = false;
            }
        }
        j++;
        i++;
    } while (!is_sorted);

    for (auto a : A)
        cout << a << ' ';
    return 0;
}