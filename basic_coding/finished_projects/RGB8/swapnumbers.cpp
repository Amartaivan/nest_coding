#include <iostream>

using namespace std;

int main(){

    int n, k, swap1, swap2, i;
    cin >> n >> k;

    int A[n];
    for (i = 0; i < n; i++)
        A[i] = i + 1;

    for (i = 0; i < k; i++){
        cin >> swap1 >> swap2;
        swap(A[swap1 - 1], A[swap2 - 1]);
    }

    for (int a : A)
        cout << a << ' ';
    return 0;
}
