using System;
using System.IO;
using System.Text;

namespace TestTask
{
    public class ReadOnlyStream : IReadOnlyStream
    {
        private const string __errorNoMoreSymbols = "Нет символов для чтения";
        private const string __errorStreamSeekNotSupport = "Файловый поток не поддерживает перемещение";
        private const string __errorFileOpen = "Ошибка открытия файла '{0}'.";

        private readonly Stream _localStream;
        private readonly Encoding _encoding;
        private bool _disposed = false;

        /// <summary>
        /// Конструктор класса. 
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public ReadOnlyStream(string fileFullPath, Encoding encoding = default)
        {
            CheckPath(fileFullPath);
            _encoding = DefineEncoding(encoding);
            // TODO : Заменить на создание реального стрима для чтения файла! (заменено)
            _localStream = TryOpenStream(fileFullPath);
        }

        /// <summary>
        /// Флаг окончания файла.
        /// </summary>
        // TODO : Заполнять данный флаг при достижении конца файла/стрима при чтении (реализовано)
        public bool IsEof => _localStream.Position >= _localStream.Length;

        /// <summary>
        /// Ф-ция чтения следующего символа из потока.
        /// Если произведена попытка прочитать символ после достижения конца файла, метод 
        /// должен бросать соответствующее исключение
        /// </summary>
        /// <returns>Считанный символ.</returns>
        public char ReadNextChar()
        {
            CheckDisposed();
            // TODO : Необходимо считать очередной символ из _localStream (реализовано)
            byte[] buffer = new byte[sizeof(char)];
            int bytesRead = _localStream.Read(buffer, 0, sizeof(char));

            if (bytesRead != sizeof(char))
                throw new EndOfStreamException(__errorNoMoreSymbols);

            return _encoding.GetChars(buffer)[0];
        }

        /// <summary>
        /// Сбрасывает текущую позицию потока на начало.
        /// </summary>
        public void ResetPositionToStart()
        {
            if (!_localStream.CanSeek)
                throw new InvalidOperationException(__errorStreamSeekNotSupport); 

            _localStream.Position = 0;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _localStream.Dispose();
                _disposed = true;
            }
        }

        private void CheckDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        private Stream TryOpenStream(string fileFullPath)
        {
            try
            {
                return File.OpenRead(fileFullPath);
            }
            catch (IOException ex)
            {
                throw new IOException(string.Format(__errorFileOpen, fileFullPath), ex);
            }
        }

        private static void CheckPath(string fileFullPath)
        {
            if (string.IsNullOrWhiteSpace(fileFullPath))
                throw new ArgumentNullException(nameof(fileFullPath));
        }

        private static Encoding DefineEncoding(Encoding encoding) =>
            encoding == default
                ? Encoding.UTF8
                : encoding;
    }
}
