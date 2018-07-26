#include <iostream>
#include <vector>

using namespace std;

int main(){

    int count = 0, tmp = 1;
    vector<int> A;

    cin >> tmp;
    while (tmp != 0){
        A.push_back(tmp);
        count++;
        cin >> tmp;
    }

    cout << count << endl;
    for (int i : A)
        cout << i << ' ';
    return 0;
}