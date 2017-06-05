#include <iostream>
using namespace std;

struct Weight {
	int W[3];	/*Weight to save*/
	char sign;
};


int main()
{
	int s[3];/*Initial Wight Array*/
	int sum = 0;/*sum*/
	int count = 0;/*correct count*/
	int cycle = 1;
	struct Weight I[4];/*count of weight*/

	cout << "Initial Wight:" << endl;
	for (int i = 0; i < 3; i++)
	{
		cin >> s[i];
	}
	cout <<endl<< "Wight You Known: ";
	for (int i = 0; i < 4; i++)/*Input the wieght*/
	{
		cout << endl << "I" << i + 1 << ": "<<endl;
		for (int j = 0; j < 3; j++)
		{
			cin >> I[i].W[j];
		}
		cin >> I[i].sign;
	}
	cout << "        Chosen Instance  Current Weight Vector" << endl;
	cout << "Cycle 0       (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	while (count != 4)/*if correct count is 4 then circle*/
	{
		count = 0;
		for (int i = 0; i < 4; i++) 
		{
			sum = 0;
			for (int j = 0; j < 3; j++)
			{
				sum += s[j] * I[i].W[j];
			}
			if (sum <= 0 && I[i].sign == '+')/*if the sign is positive, result <= 0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] += I[i].W[j];
				}
			}
			else if (sum >= 0 && I[i].sign == '-')/*if the sign is negative,result >= 0*/
			{
				for (int j = 0; j < 3; j++)
				{
					s[j] -= I[i].W[j];
				}
			}
			else
			{
				count++;/*correct count + 1*/
			}
			if (count != 4)/*if count != 4,keep circle*/
			{
				cout << "Cycle " << cycle << "  (" << I[i].W[0] << "," << I[i].W[1] << "," << I[i].W[2] << "," << I[i].sign << ")";
				cout << "     (" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
				cout << endl;
				cycle++;
			}
		}
	}
	cout << "Answer:" << "(" << s[0] << "," << s[1] << "," << s[2] << ")" << endl;
	cout << endl;
	system("pause");		
}
