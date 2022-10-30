# простой python скрипт
# для заполнения таблица случайными значениями
# в нужном количестве

from ast import main
import random
import sqlite3

# defines
database_file = 'ntsDB.db'
row_count = 1000

def create_connection(db_file):
    conn = None

    try:
        conn = sqlite3.connect(db_file)
    except Error as e:
        print(e)

    return conn

def insert_price(conn, price):
    sql = """INSERT INTO prices(price) 
             VALUES (?);"""
    cur = conn.cursor()
    cur.execute(sql, price)
    conn.commit()
    return cur.lastrowid

def insert_product(conn, product):
    sql = """INSERT INTO products(code, name, bar_code, quantity, model, sort, color, size, wight, date_changes, id_price)
             VALUES (?,?,?,?,?,?,?,?,?, DATETIME('now'),?);"""
    cur = conn.cursor()
    cur.execute(sql, product)
    conn.commit()
    return cur.lastrowid

def get_random_product():

    # code
    code = random.randint(1000, 100000)
    
    # name
    name = '' 
    word_pieces = ('ha', 'cen', 'bo', 'let', 'feel', 'ca', 'pu', 'se', 'ga',
                   'bu', 'ch', 'fe', 'co', 'ca', 'ten', 'so', 'me')
    for x in range(random.randint(2, 5)):
        name += random.choice(word_pieces)
    
    # bar_code
    bar_code = ''
    for x in range(12):
        bar_code += str(random.randint(0, 9))

    # quantity
    quantity = random.randint(5, 100)

    # model
    model = ''
    model_pieces = ('X', 'J', '1', '0','9', '2', '3', 'K', 'V', 'S')
    for x in range(7):
        model += random.choice(model_pieces)

    # sort
    sort = 'None'

    # color 
    colors = ('White', 'Red', 'Blue', 'Green', 'Black', 'Yellow', 'Brown', 'Grey')
    color = random.choice(colors)

    # size 
    sizes = ('Small', 'Medium', 'Large')
    size = random.choice(sizes)

    # wight
    wight = 'None'

    # id_price
    id_price = random.randint(1, 5)

    product_list = list()
    product_list.append(code)
    product_list.append(name)
    product_list.append(bar_code)
    product_list.append(quantity)
    product_list.append(model)
    product_list.append(sort)
    product_list.append(color)
    product_list.append(size)
    product_list.append(wight)
    product_list.append(id_price)

    return tuple(product_list)

def clear_table(conn):
    cur = conn.cursor()
    cur.execute("DELETE FROM products;")
    print("We have deleted ", cur.rowcount, " records from table 'products'")
    conn.commit()

def update_sequence(conn):
    cur = conn.cursor()
    cur.execute("""UPDATE `sqlite_sequence`
                   SET `seq` = (SELECT MAX(`id`) FROM 'products')
                   WHERE `name` = 'products';""")
    conn.commit()

def main():
    conn = create_connection(database_file)
    print(conn)
    with conn:
        clear_table(conn)
        update_sequence(conn)
        for x in range(row_count):
            product = get_random_product()
            product_id = insert_product(conn, product)

if __name__ == '__main__':
    main()