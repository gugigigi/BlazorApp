# BlazorApp
学習用BlazorApp

Dockerで BlazorServer + Postgre環境 を再現する構築環境の方法になります。
Docker上でBlazorApp(8080:8080)とDB(5432:5432)環境を実行できる用にさせてます。
※環境構築のみで、DBテーブルの作成とBlazorとの紐付けはまだ出来ていません。

実行環境
Docker
.Net6.0
BlazorServer
Postgresql

DBサーバー情報
host:localhost
DB:test_db
User:postgres
container_name: postgres
password:postgres

参考文献
https://github.com/ymd65536/BlazorAppContainer/tree/main
https://qiita.com/kurkuru/items/b44653a5fcd1072852c0

.Net6.0 + BlazorServer での実行環境
https://blog.unikktle.com/blazor-server%e3%81%8b%e3%82%89-postgresql%e3%81%b8sql%e6%96%87%e3%82%92%e7%99%ba%e8%a1%8c%e3%81%99%e3%82%8b/

環境構築
ターミナルから、以下のコマンドを叩いてDockerを構築・起動

/*
：Blazorappの起動
docker build -t blazorappcontainer . --no-cache
docker run --rm -p 8080:8080 --name blazorappcontainer blazorappcontainer
*/

：Postgresのコンテナ起動
    docker compose up -d
：コンテナ内で動いているDBに接続
    docker exec -it postgres bash
:Docker接続 -> psqlに接続する
    root@a8834891645b:/# psql -U user

VisualStudioからIIS EXPRESSを起動

：デモ用のテーブルの作成
================================================================================
試しにpsqlでテーブル(m_table_a)を作成した時のコマンド

user=# CREATE DATABASE test_db;
CREATE DATABASE
user=# \c test_db; 
You are now connected to database "test_db" as user "postgres".
test_db=#  CREATE TABLE m_table_a (id integer, code varchar(10), value_string varchar(20));
CREATE TABLE
test_db=# \dt
         List of relations
 Schema |   Name   | Type  | Owner 
--------+----------+-------+-------
 public | m_table_a | table | user
(1 row)

test_db=#  INSERT INTO m_table_a (id, code, value_string) VALUES (1, 'Taro', 'ABC Co. Ltd.');
INSERT 0 1

test_db=# SELECT * FROM m_table_a;

================================================================================

================================================================================
#ボトルネック -- Mac環境では構築が難しい箇所があったので追記しております。

現在、Mac + Postgre環境でのログイン機能を実装する作業において問題があります。
以下のサイトを参考にログイン機能を実装させたかったのですが
https://qiita.com/Meister619/items/9219015f1ac1a524c4da

Mac準拠の VisualStudio for Mac はサービス終了に伴い .Net8.0 が使えない問題が発生しました。
(
    そもそも、VisualStudioのMac版は2024年をもってサービス終了とのこと
    https://visualstudio.microsoft.com/ja/vs/mac/
)

よって、Mac機でWindows準拠のBlazorを動かすのは難しいかもしれません。
※単に余計な調査のコストがかかってしまう。
なので、以降はWindows機で作業を推奨します。

================================================================================

================================================================================
帳票機能実装にあたっての課題点
ボタン押下後のページ遷移は BlazorServerだとうまく遷移できませんでした .NavigateTo("/~",true,false); を設定し
あらかじめforceLoadを true変え 回避させています。
※BlazorWebAssenblyでは通常通り遷移可能のようでした。

================================================================================
