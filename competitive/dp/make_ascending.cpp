#include <iostream>
#include <vector>

using namespace std;

int calculate(vector<int> a, int i, int j){
    if (i == j)
        return 0;
    if (a[i] > a[i + 1]){
        a[i + 1] = a[i];
        return calculate(a, i + 1, j) + 1;
    }
    else
        return calculate(a, i + 1, j);
}

int main(){

    int n;
    cin >> n;

    vector<int> a(n);
    for (int i = 0; i < n; i++)
        cin >> a[i];

    cout << calculate(a, 0, a.size() - 1) << endl;
    return 0;
}