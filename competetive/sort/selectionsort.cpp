#include <iostream>

using namespace std;

int main(){

    int n, min;
    cin >> n;
    int A[n];

    for (int i = 0; i < n; i++)
        cin >> A[i];

    for (int i = 0; i < n; i++){
        min = i;
        for (int j = i + 1; j < n; j++){
            if (A[min] > A[j])
                min = j;
        }
        swap(A[min], A[i]);
    }

    for (int a : A)
        cout << a << " ";
    
    cout << endl;

    return 0;
}