#include <iostream>
#include <cmath>

using namespace std;

int main(){
    int result = 0, i, n;
    cin >> n;

    int min, max;
    int* a = new int[n];
    cin >> a[0];
    min = a[0];
    max = a[0];

    for (i = 1; i < n; i++){
        cin >> a[i];
        if (a[i] > max)
            max = a[i];
        if (a[i] < min)
            min = a[i];
    }
        
    int size = max - min + 1, start = min;
    int* b = new int[size];
    for (i = 0; i < size; i++)
        b[i] = 0;
    for (i = 0; i < n; i++)
        b[a[i] - start]++;
    for (i = 0; i < size; i++)
        if (b[i] > 1)
            result += (b[i] * b[i] - b[i]) / 2;

    cout << result << endl;
    return 0;
}