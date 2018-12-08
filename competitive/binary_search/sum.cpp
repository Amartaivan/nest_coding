#include <iostream>

using namespace std;

int binary_search(int a[], int l, int r, int x){
    
    while (l <= r){
        int m = (l + r) / 2;
        
        if (a[m] == x)
            return m;
        
        if (a[m] < x)
            l = m + 1;

        if (a[m] > x)
            r = m - 1;
    }

    return -1;
}

int main(){

    int n;
    cin >> n;
    int A[n];

    for (int i = 0; i < n; i++)
        cin >> A[i];

    int target, result_j = -1, result_i = 0;
    cin >> target;

    int i = 0;
    do {
        result_j = binary_search(A, i + 1, n - 1, target - A[i]);
        result_i = i;
        i++;
    } while (i < n - 1 && result_j == -1);

    if (result_j == -1)
        cout << "NONE" << endl;
    else
        cout << result_i << ' ' << result_j << endl;
    return 0;
}