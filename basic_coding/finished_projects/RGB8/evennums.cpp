#include <iostream>

using namespace std;

int main(){

    int n, i, count = 0;
    cin >> n;

    int* a = new int[n];

    for (i = 0; i < n; i++){
        cin >> a[i];
        if (i % 2 == 1 && a[i] % 2 == 0) //(i % 2 == 1) == ((i + 1) % 2 == 0)
            count++;
    }

    cout << count << endl;
    return 0;
}