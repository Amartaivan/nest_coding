#include <iostream>

using namespace std;

int main(){
	
	int n, m, i, tmp;
	cin >> n >> m;
	int A[101];
	
	for (i = 0; i < 101; i++)
		A[i] = 0;

	for (i = 0; i < m; i++){
		cin >> tmp;
		A[tmp]++;
		cin >> tmp;
		A[tmp]++;
	}
	
	for (i = 0; i < n; i++)
		cout << A[i + 1] << ' ';
	return 0;
}
