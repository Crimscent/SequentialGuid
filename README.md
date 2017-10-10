# SequentialGuid
Sequential guid generator.

Format is as follows:

<table>
  <tr>
    <td colspan="4">counter</td>
    <td colspan="2">process id</td>
    <td colspan="4">machine id</td>
    <td colspan="4">unix epoch time</td>
    <td colspan="2">empty</td>
  </tr>
  <tr>
    <td>0</td>
    <td>1</td>
    <td>2</td>
    <td>3</td>
    <td>4</td>
    <td>5</td>
    <td>6</td>
    <td>7</td>
    <td>8</td>
    <td>9</td>
    <td>10</td>
    <td>11</td>
    <td>12</td>
    <td>13</td>
    <td>14</td>
    <td>15</td>
  </tr>
</table>

* unix epoch time - number of seconds since 01.01.1970, big endian order
* machine id - first 4 bytes of MD5 hash taken from machine name, order 8-9-6-7
* process id - first 2 bytes of process identifier, order 4-5
* counter - auto-incrementing overflow integer counter, start value is randomly generated during process startup, big endian order
