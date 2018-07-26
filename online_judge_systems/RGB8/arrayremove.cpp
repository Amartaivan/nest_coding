#include <iostream>
#include <vector>

using namespace std;

int main(){

    vector<int> A;
    int n, tmp, i, m;
    cin >> n;
    for (i = 0; i < n; i++)
    {
        cin >> tmp;
        A.push_back(tmp);
    }
    cin >> m;

    A.erase(A.begin() + m - 1);

    for (int buf : A)
        cout << buf << ' ';
    return 0;
}