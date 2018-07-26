//O(n^3)
#include <iostream>

using namespace std;

int main(){

    int n, i, j, k, max, maxi = 0, maxj = 1, maxk = 2;
    cin >> n;
    int* a = new int[n];

    for (i = 0; i < n; i++)
        cin >> a[i];
    max = a[0] * a[1] * a[2];

    for (i = 0; i < n; i++)
        for (j = i + 1; j < n; j++)
            for (k = j + 1; k < n; k++)
                if (a[i] * a[j] * a[k] > max){
                    max = a[i] * a[j] * a[k];
                    maxi = i;
                    maxj = j;
                    maxk = k;
                }

    cout << a[maxi] << ' ' << a[maxj] << ' ' << a[maxk] << endl;
    return 0;
}