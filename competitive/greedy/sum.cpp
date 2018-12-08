#include <algorithm>
#include <iostream>
#include <vector>

using namespace std;

int main(){

    int n, k;
    cin >> n;

    vector<int> nums(n);
    for (int i = 0; i < n; i++)
        cin >> nums[i];
    cin >> k;

    sort(nums.begin(), nums.end());

    int l = 0, r = 0;
    while (l < n){
        int sum = 0;
        for (int i = l; i <= r; i++)
            sum += nums[i];

        if (sum == k){
            for (int i = l; i <= r; i++)
                cout << nums[i] << ' ';
            cout << endl;
            r++;
        }
        else if (sum > k)
            l++;
        else if (sum < k){
            if (r < n)
                r++;
            else
                break;
        }
    }
    return 0;
}