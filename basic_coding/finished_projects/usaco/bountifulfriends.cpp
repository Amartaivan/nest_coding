#include <iostream>
#include <vector>
#include <string>

using namespace std;

int indexof(string name, vector<string> names){

    for (int i = 0; i < names.size(); i++)
        if (names[i] == name)
            return i;
    return -1;
}

int main(){

    int n;
    cin >> n;

    vector<string> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];

    vector<int> D(n, 0); //balances
    string name, tmpname;
    int k, m, tmp, tmp1;

    for (int i = 0; i < n; i++){
        cin >> name;

        tmp = indexof(name, A);
        cin >> m >> k;

        if (k > 0){
            D[tmp] -= m;
            D[tmp] += m % k;
            m /= k;

            for (int i = 0; i < k; i++){
                cin >> tmpname;

                tmp1 = indexof(tmpname, A);
                D[tmp1] += m;
            }
        }
    }

    for (int i = 0; i < n; i++)
        cout << A[i] << ' ' << D[i] << endl;
    return 0;
}