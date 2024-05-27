# PalindromeChecker
<p>Клиент-серверное приложение (.NET 8.0) для проверки строк на палиндромы (тестовое задание Macroscop)</p>
<p>Клиентская часть (WPF):</p>
<ol>
  <li>Ввод адреса серверной части;</li>
  <li>Выбор папки с текстовыми файлами для проверки на палиндромы (как ручным вводом, так и через проводник ОС);</li>
  <li>Соединение с серверной частью и отправка текста файлов на проверку;</li>
  <li>Один файл соответствует одному запросу на сервер (клиент отправляет сразу множество запросов);</li>
  <li>В случае перегрузки сервера, клиент ожидает 1 секунду и пробует отправить запрос снова;</li>
  <li>Отображение данных о результатах проверки и ошибках (при наличии).</li>
</ol>
<p>Серверная часть (консоль, Web API):</p>
<ol>
  <li>Ввод максимально количества одновременно обрабатываемых запросов (параметр N);</li>
  <li>При превышении параметра N на все последующие запросы сервер возвращает 503 код ошибки;</li>
  <li>На вход принимает строку для проверки на палиндром, возвращает true на палиндром и false в противном случае;</li>
  <li>Проверка строки осуществляется только по буквам и цифрам (пробелы и другие символы пунктуации не учитываются);</li>
  <li>Для имитации длительной обработки используется ожидание длиной 2 секунды.</li>
</ol>
