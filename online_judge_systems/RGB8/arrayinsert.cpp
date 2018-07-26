#include <iostream>
#include <vector>

using namespace std;

int main(){

    vector<int> A;
    int n, tmp, b, c, i;
    cin >> n;

    for (i = 0; i < n; i++){
        cin >> tmp;
        A.push_back(tmp);
    }
    cin >> b >> c;

    A.insert(A.begin() + b - 1, c);
    for (int buf : A)
        cout << buf << ' ';
    return 0;
}