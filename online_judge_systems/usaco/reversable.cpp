#include <algorithm>
#include <iostream>
#include <cstring>
#include <vector>

using namespace std;

bool isreversable(int N, int B){
    vector<char> tmp;
    while (N > 0){
        if (N % B > 9)
            tmp.push_back('A' + N % B - 10);
        else
            tmp.push_back('0' + N % B);
        N /= B;
    }
    
    bool res = true;
    for (int i = 0; i < tmp.size() / 2; i++)
        if (tmp[i] != tmp[tmp.size() - i - 1])
            res = false;

    return res;
}

int main(){
    int B;
    cin >> B;

    for (int i = 1; i <= 300; i++){
        if (isreversable(i * i, B))
            cout << i << ' ' << i * i << endl;
    }
    return 0;
}