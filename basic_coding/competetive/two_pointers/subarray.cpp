#include <iostream>
#include <vector>
#include <map>

using namespace std;

int main(){

    int n, k, result = 0;
    cin >> n >> k;

    vector<int> array(n);
    for (int i = 0; i < n; i++)
        cin >> array[i];

    int l = 0, r = 0;
    while (l < n){
        map<int, int> count;
        while (r < n){
            if (count.find(array[r]) == count.end())
                count.insert(make_pair(array[r], 1));
            else {
                count[array[r]]++;
                if (count[array[r]] == k)
                    break;
            }
            r++;
        }
        if (r < n)
            result += n - r;
        count.erase(array[l]);
        l++;
    }
    cout << result << endl;
    return 0;
}